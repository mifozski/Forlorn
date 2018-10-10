using Forlorn;

namespace Forlorn
{
    static public class Reducers
    {
        public static Redux.Reducer scene = (object state, object action) => {
            if (action.GetType().FullName == "Redux+INITIAL_ACTION")
            {
                return 0;
            }
            if (action.GetType().Name == "LOAD_SCENE")
            {

            }
            return state;
        };
    }

    class Store
    {


        Store()
        {
            Redux.FinalReducer finalReducer = Redux.combineReducers(new Redux.Reducer[] {
                Reducers.scene
            });

            Redux.Store store = Redux.createStore(finalReducer);
        }
    }
}