extends Area2D

var isMouseEntered = false

func _ready() -> void:
	self.add_to_group("Space")
	
func _on_mouse_entered() -> void:
	isMouseEntered = true

func _on_mouse_exited() -> void:
	isMouseEntered = false

func grabPiece() -> Vector2:
	if isMouseEntered:
		print("squaregrabber")
		return self.global_position
	else:
		return Vector2(12,12)
