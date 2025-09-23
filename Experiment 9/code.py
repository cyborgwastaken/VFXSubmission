import bpy
import random
from mathutils import Vector
from math import pi, sin, cos

# ---------------------------
# Clear existing scene
# ---------------------------
bpy.ops.object.select_all(action='SELECT')
bpy.ops.object.delete(use_global=False)

# ---------------------------
# Scene settings
# ---------------------------
NUM_RIGID = 5
NUM_SEMI_RIGID = 5
DROP_HEIGHT = 8.0
SIM_FRAMES = 250
GRAVITY = -9.81

scene = bpy.context.scene
scene.frame_start = 1
scene.frame_end = SIM_FRAMES
scene.frame_set(1)
scene.gravity = (0, 0, GRAVITY)

# Ensure Rigid Body World
if not scene.rigidbody_world:
    bpy.ops.rigidbody.world_add()
scene.rigidbody_world.time_scale = 1.0
scene.rigidbody_world.point_cache.frame_step = 1

# ---------------------------
# Ground plane
# ---------------------------
bpy.ops.mesh.primitive_plane_add(size=20, location=(0, 0, 0))
ground = bpy.context.active_object
ground.name = "Ground"
bpy.ops.rigidbody.object_add()
ground.rigid_body.type = 'PASSIVE'
ground.rigid_body.friction = 0.5
ground.rigid_body.restitution = 0.0

# ---------------------------
# Helper functions
# ---------------------------
def create_cube(name, location, size=0.5):
    bpy.ops.mesh.primitive_cube_add(size=size, location=location)
    cube = bpy.context.active_object
    cube.name = name
    return cube

def add_rigid_body(obj, mass=1.0, color=(1,0,0,1)):
    bpy.ops.rigidbody.object_add()
    obj.rigid_body.type = 'ACTIVE'
    obj.rigid_body.mass = mass
    obj.rigid_body.friction = 0.3
    obj.rigid_body.restitution = 0.4
    # Set material color
    if not obj.data.materials:
        mat = bpy.data.materials.new(name=f"{obj.name}_Mat")
        obj.data.materials.append(mat)
    obj.data.materials[0].diffuse_color = color

def connect_spring(obj1, obj2, stiffness=50, damping=5):
    # Add empty to host constraint
    mid = (obj1.location + obj2.location) / 2
    bpy.ops.object.empty_add(type='PLAIN_AXES', location=mid)
    con_empty = bpy.context.active_object
    bpy.ops.rigidbody.constraint_add()
    con = con_empty.rigid_body_constraint
    con.type = 'GENERIC_SPRING'
    con.object1 = obj1
    con.object2 = obj2
    # Enable springs
    con.use_spring_x = True
    con.use_spring_y = True
    con.use_spring_z = True
    con.spring_stiffness_x = stiffness
    con.spring_stiffness_y = stiffness
    con.spring_stiffness_z = stiffness
    con.spring_damping_x = damping
    con.spring_damping_y = damping
    con.spring_damping_z = damping
    return con

# ---------------------------
# Create rigid debris
# ---------------------------
rigid_debris = []
for i in range(NUM_RIGID):
    x = random.uniform(-5, 5)
    y = random.uniform(-5, 5)
    z = DROP_HEIGHT
    cube = create_cube(f"Rigid_{i+1}", (x, y, z))
    add_rigid_body(cube, mass=3, color=(1,0,0,1))  # Red
    rigid_debris.append(cube)

# ---------------------------
# Create semi-rigid debris (chain with springs)
# ---------------------------
semi_rigid_debris = []
radius = 0.3
center_x, center_y = 0, 5
for i in range(NUM_SEMI_RIGID):
    angle = 2*pi*i/NUM_SEMI_RIGID
    x = center_x + cos(angle)*radius*NUM_SEMI_RIGID
    y = center_y + sin(angle)*radius*NUM_SEMI_RIGID
    z = DROP_HEIGHT
    cube = create_cube(f"SemiRigid_{i+1}", (x, y, z))
    add_rigid_body(cube, mass=1.5, color=(0,0,1,1))  # Blue
    semi_rigid_debris.append(cube)

# Connect consecutive cubes with springs
for i in range(NUM_SEMI_RIGID-1):
    connect_spring(semi_rigid_debris[i], semi_rigid_debris[i+1], stiffness=50, damping=5)

# Optional: connect last to first to make a ring
# connect_spring(semi_rigid_debris[-1], semi_rigid_debris[0], stiffness=50, damping=5)

# ---------------------------
# Add camera
# ---------------------------
bpy.ops.object.camera_add(location=(0, -15, 10), rotation=(1.1,0,0))
scene.camera = bpy.context.active_object

print("Setup complete! Red = rigid, Blue = semi-rigid chain.")
print("Press Spacebar to play simulation. Bake cache for consistent playback.")
