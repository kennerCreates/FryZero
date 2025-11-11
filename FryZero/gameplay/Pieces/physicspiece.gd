extends RigidBody2D

func _ready() -> void:
	self.collision_layer = 9
	self.z_index = 10
	
func _movement_detected(movement: bool):
	if movement ==  true:
		_pickup_piece()
	else:
		_drop_piece()

func _drop_piece():
	self.collision_layer = 9
	self.z_index = 10

	
func _pickup_piece():
	self.collision_layer = 10
	self.z_index = 20
	
