extends Node2D

func _input(event: InputEvent) -> void:
	if event is InputEventMouseButton: 
		if event.button_index == MOUSE_BUTTON_LEFT:
			if event.pressed:
				mousePressed()
			else:
				mouseReleased()

func mousePressed ():
	print("mouse clicked")


func mouseReleased ():
	print("mouse released")
