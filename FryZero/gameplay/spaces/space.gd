extends Area2D

var isMouseEntered = false
var spaceName

func _ready() -> void:
	self.add_to_group("Space")
	
func getSpaceName():
	rank = GlobalRef.getScreenX()
	GlobalRef.getSpaceString()

func _on_mouse_entered() -> void:
	isMouseEntered = true

func _on_mouse_exited() -> void:
	isMouseEntered = false

func grabPiece():
	if isMouseEntered:
		print(spaceName)
	
