using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Cradle;
using Cradle.StoryFormats.Harlowe;
using TMPro;

[System.Serializable]
public class StringUnityEvent : UnityEvent<string> {}

namespace Cradle.Players
{
	[ExecuteInEditMode]
	public class TwineTextPlayer : MonoBehaviour
	{
		public Story Story;
		public RectTransform Container;
		public Button LinkTemplate;
		public TextMeshProUGUI WordTemplate;
		public RectTransform LineBreakTemplate;
		public bool StartStory = true;
		public bool AutoDisplay = true;
		public bool ShowNamedLinks = true;
		public string startingPassage = "";

		public UnityEvent DialogueEnded;
		public StringUnityEvent CallFunc;

		private List<StoryOutput> prePausedOutputs = new List<StoryOutput>();
		private bool paused = false;

		private string[] currentTags;

		static Regex rx_splitText = new Regex(@"(\s+|[^\s]+)");

		void Start()
		{
			if (!Application.isPlaying)
				return;

			LinkTemplate.gameObject.SetActive(false);
			((RectTransform)LinkTemplate.transform).SetParent(null);
			LinkTemplate.transform.hideFlags = HideFlags.HideInHierarchy;

			WordTemplate.gameObject.SetActive(false);
			WordTemplate.rectTransform.SetParent(null);
			WordTemplate.rectTransform.hideFlags = HideFlags.HideInHierarchy;

			LineBreakTemplate.gameObject.SetActive(false);
			LineBreakTemplate.SetParent(null);
			LineBreakTemplate.hideFlags = HideFlags.HideInHierarchy;

			if (this.Story == null)
				this.Story = this.GetComponent<Story>();
			if (this.Story == null)
			{
				Debug.LogError("Text player does not have a story to play. Add a story script to the text player game object, or assign the Story variable of the text player.");
				return;
			}

			this.Story.OnPassageEnter += Story_OnPassageEnter;
			this.Story.OnOutput += Story_OnOutput;
			this.Story.OnOutputRemoved += Story_OnOutputRemoved;
			this.Story.OnPassageDone += Story_OnPassageDone;

			if (StartStory)
			{
				BeginStory(startingPassage);
			}
		}

		void OnDestroy()
		{
			if (!Application.isPlaying)
				return;

			if (this.Story != null)
			{
				this.Story.OnPassageEnter -= Story_OnPassageEnter;
				this.Story.OnOutput -= Story_OnOutput;
			}
		}

		// .....................
		// Clicks

#if UNITY_EDITOR
		void Update()
		{
			if (Application.isPlaying)
				return;

			// In edit mode, disable autoplay on the story if the text player will be starting the story
			if (this.StartStory)
			{
				foreach (Story story in this.GetComponents<Story>())
					story.AutoPlay = false;
			}
		}
#endif

		public void Clear()
		{
			for (int i = 0; i < Container.childCount; i++)
				GameObject.Destroy(Container.GetChild(i).gameObject);
			Container.DetachChildren();
		}

		void Story_OnPassageEnter(StoryPassage passage)
		{
			prePausedOutputs.Clear();
			Debug.Log($"STARTING A NEW PASSAGE: {passage.Name}");
			Debug.Log($"\tWITH TAGS: {string.Join(", ", passage.Tags)}");
			Clear();
			currentTags = passage.Tags;

			foreach (string tag in currentTags)
			{
				if (tag.StartsWith("call_"))
				{
					Debug.Log($"CALLING FUNC {tag.Substring(5)}");
					CallFunc.Invoke(tag.Substring(5));
				}
				else if (tag.Equals("pause_on_begin"))
				{
					Debug.Log("Story is paused");
					paused = true;
				}
			}
		}

		void Story_OnOutput(StoryOutput output)
		{
			if (!this.AutoDisplay)
				return;

			DisplayOutput(output);
		}

		void Story_OnOutputRemoved(StoryOutput outputThatWasRemoved)
		{
			// Remove all elements related to this output
			foreach (var elem in Container.GetComponentsInChildren<TwineTextElement>()
				.Where(e => e.SourceOutput == outputThatWasRemoved))
			{
				elem.transform.SetParent(null);
				GameObject.Destroy(elem.gameObject);
			}
		}

		void Story_OnPassageDone(StoryPassage passage)
		{
			if (currentTags.Contains("end_dialogue"))
			{
				Button btn = AddButton("end_dialogue", "--------------------------x--------------------------", null);
				btn.onClick.AddListener(() =>
				{
					DialogueEnded.Invoke();
				});
			}
		}

		int GetInsertIndex(StoryOutput output)
		{
			// Deternine where to place this output in the hierarchy - right after the last UI element associated with the previous output, if exists
			TwineTextElement last = Container.GetComponentsInChildren<TwineTextElement>()
				.Where(elem => elem.SourceOutput.Index < (output != null ? output.Index : 100))
				.OrderBy(elem => elem.SourceOutput.Index)
				.LastOrDefault();
			int uiInsertIndex = last == null ? -1 : last.transform.GetSiblingIndex() + 1;

			return uiInsertIndex;
		}

		public void DisplayOutput(StoryOutput output)
		{
			if (paused)
			{
				prePausedOutputs.Add(output);
				return;
			}

			int uiInsertIndex = GetInsertIndex(output);

			// Temporary hack to allow other scripts to change the templates based on the output's Style property
			SendMessage("Twine_BeforeDisplayOutput", output, SendMessageOptions.DontRequireReceiver);

			if (output is StoryText)
			{
				var text = (StoryText)output;
				if (!string.IsNullOrEmpty(text.Text))
				{
					foreach (Match m in rx_splitText.Matches(text.Text))
					{
						string word = m.Value;
						if (word == " ")
						{
							word = ((char)0x00A0).ToString(); // non-break space
						}
						TMPro.TextMeshProUGUI uiWord = (TMPro.TextMeshProUGUI)Instantiate(WordTemplate);
						uiWord.gameObject.SetActive(true);
						uiWord.text = word;
						uiWord.name = word;
						AddToUI(uiWord.rectTransform, output, uiInsertIndex);
						if (uiInsertIndex >= 0)
							uiInsertIndex++;
					}
				}
			}
			else if (output is StoryLink)
			{
				var link = (StoryLink)output;
				if (!ShowNamedLinks && link.IsNamed)
					return;

				Debug.Log($"LINK: {link.Text}");
				Button uiLink;
				if (link.Text == "continue")
				{
					 uiLink = AddButton("continue", "...", output);
				}
				else
				{
					uiLink = (Button)Instantiate(LinkTemplate);
					uiLink.gameObject.SetActive(true);
					uiLink.name = "[[" + link.Text + "]]";

					TextMeshProUGUI uiLinkText = uiLink.GetComponentInChildren<TextMeshProUGUI>();
					uiLinkText.text = link.Text;
				}

				uiLink.onClick.AddListener(() =>
				{
					this.Story.DoLink(link);
				});
				AddToUI((RectTransform)uiLink.transform, output, uiInsertIndex);
			}
			else if (output is LineBreak)
			{
				var br = (RectTransform)Instantiate(LineBreakTemplate);
				br.gameObject.SetActive(true);
				br.gameObject.name = "(br)";
				AddToUI(br, output, uiInsertIndex);
			}
			else if (output is StyleGroup)
			{
				// Add an empty indicator to later positioning
				var groupMarker = new GameObject();
				groupMarker.name = output.ToString();
				AddToUI(groupMarker.AddComponent<RectTransform>(), output, uiInsertIndex);
			}
		}

		void AddToUI(RectTransform rect, StoryOutput output, int index)
		{
			rect.SetParent(Container);
			if (index >= 0)
				rect.SetSiblingIndex(index);

			var elem = rect.gameObject.AddComponent<TwineTextElement>();
			elem.SourceOutput = output;
		}

		Button AddButton(string name, string text, StoryOutput output)
		{
			Button uiEndDialogueButton = (Button)Instantiate(LinkTemplate);
			uiEndDialogueButton.gameObject.SetActive(true);
			uiEndDialogueButton.name = "[[" + name + "]]";

			TextMeshProUGUI uiLinkText = uiEndDialogueButton.GetComponentInChildren<TextMeshProUGUI>();
			uiLinkText.text = text;
			uiLinkText.alignment = TextAlignmentOptions.Center;

			AddToUI((RectTransform)uiEndDialogueButton.transform, output, GetInsertIndex(output));

			return uiEndDialogueButton;
		}

		public void BeginStory(string passageName)
		{
			if (passageName != "")
			{
				Story.GoTo(passageName);
			}
			else
			{
				Story.Begin();
			}
		}

		public void ContinuePausedStory()
		{
			paused = false;
			prePausedOutputs.ForEach(output => Story_OnOutput(output));
		}
	}
}