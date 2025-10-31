extends Node2D

signal leftMouseClick 
signal leftMouseRelease

func _input(event: InputEvent) -> void:
	if event is InputEventMouseButton: 
		if event.button_index == MOUSE_BUTTON_LEFT:
			if event.pressed:
				emit_signal("leftMouseClick")
				#mousePressed()
			else:
				emit_signal("leftMouseRelease")
				#mouseReleased()

func mousePressed():
	print("mouse clicked")


func mouseReleased():
	print("mouse released")
