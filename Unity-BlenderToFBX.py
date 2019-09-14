import bpy
blender280 = (2,80,0) <= bpy.app.version

if not blender280:
    try:
        import io_scene_fbx.export_fbx
    except:
        print('error: io_scene_fbx.export_fbx not found.')
        # This might need to be bpy.Quit()
        raise

# Find the Blender output file
import os
outfile = os.getenv("UNITY_BLENDER_EXPORTER_OUTPUT_FILE")

# Do the conversion
print("Starting blender to FBX conversion " + outfile)

# SORTING HELPERS (sort list of objects, parents prior to children)
# root object -> 0, first child -> 1, ...
def myDepth(o):
	if o == None:
		return 0
	if o.parent == None:
		return 0
	else:
		return 1 + myDepth(o.parent)

# compare: parent prior child
def myDepthCompare(a,b):
	da = myDepth(a)
	db = myDepth(b)
	if da < db:
		return -1
	elif da > db:
		return 1
	else:
		return 0

# Operator HELPER
class FakeOp:
	def report(self, tp, msg):
		print("%s: %s" % (tp, msg))

if blender280:
    import bpy.ops
    bpy.ops.export_scene.fbx(filepath=outfile,
        check_existing=False,
        use_selection=False,
        use_active_collection=False,
        object_types= {'ARMATURE','CAMERA','LIGHT','MESH','EMPTY'},
        use_mesh_modifiers=True,
        mesh_smooth_type='OFF',
        use_custom_props=True,
        apply_scale_options='FBX_SCALE_ALL')
else:
    # blender 2.58 or newer
    import math
    from mathutils import Matrix
    from functools import cmp_to_key

    matPatch = Matrix.Rotation(math.radians(180.0), 4, 'Z')
    matPatch = matPatch * Matrix.Rotation(math.radians(90.0), 4, 'X')

    for obj in bpy.data.objects:
        if obj.parent != None:
            continue
        obj.matrix_world = matPatch * obj.matrix_world
    # apply all(!) transforms, parents before children
    for obj in sorted(bpy.data.objects, key=cmp_to_key(myDepthCompare)):
        obj.select = True
        # apply transform
        bpy.ops.object.transform_apply(rotation=True)
        # deselect again
        obj.select = False

    for obj in bpy.data.objects:
        obj.select = False if obj.name[0] in '_.' else True

    print('HENLO')

    # kwargs = io_scene_fbx.export_fbx.defaults_unity3d()
    # custom args
    # all possible args listed here: https://www.blender.org/api/blender_python_api_2_75_1/bpy.ops.export_scene.html
    kwargs = dict(	# we dont need the rotation in the exporter anymore
        # global_matrix=Matrix.Rotation(-math.pi / 2.0, 4, 'X'),
        use_selection=True,
        # we need 'EMPTY' for placeholder-transforms and helpers -- but artists can choose to not export them via UNITY_EXPORT flag
        object_types={'ARMATURE', 'MESH', 'EMPTY'},
        use_mesh_modifiers=True,
        use_armature_deform_only=True,
        use_anim=True,
        use_anim_optimize=False,
        use_anim_action_all=True,
        batch_mode='OFF',
        # TODO: used only for animated objects without bones -- artists have to define this manually
        # Note: the animation data in the default take will also not be rotated correctly
        use_default_take=False,
    )
    kwargs["use_selection"] = True

    io_scene_fbx.export_fbx.save(FakeOp(), bpy.context, filepath=outfile, **kwargs)
    # HQ normals are not supported in the current exporter

print("Finished blender to FBX conversion " + outfile)
