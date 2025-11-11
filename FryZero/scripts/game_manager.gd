extends Node2D

#var spaceScene = preload("res://gameplay/spaces/space.tscn")

#func _ready() -> void:
	#createSpaces()

func _input(event: InputEvent) -> void:
	if event is InputEventMouseButton: 
		if event.button_index == MOUSE_BUTTON_LEFT:
			if event.pressed:
				get_tree().call_group("LeftClick","pickupPiece")
				#mousePressed()
			else:
				get_tree().call_group("LeftClick","dropPiece")
				#mouseReleased()
				
#func createSpaces(): 
	#for Key in GlobalRef.spaces:
		#var current_space = spaceScene.instantiate()
		#current_space.global_position = GlobalRef.spaces[Key]
		#current_space.spaceName = Key
		#get_node("board").add_child(current_space)

func mousePressed():
	print("mouse pressed")
	
func mouseReleased():
	print("mouse released")
