extends Area2D

signal movingPiece(bool)

var isMouseEntered = false
var isDragging = false
var delay = 10

func _ready() -> void:
	self.add_to_group("LeftClick")

func _physics_process(delta: float):
	if isDragging == true:
		var tween = get_tree().create_tween()
		tween.tween_property(self, "position", get_global_mouse_position(), delay * delta)

func pickupPiece():
	if isMouseEntered:
		isDragging = true
		movingPiece.emit(true)
		
func dropPiece():
		isDragging = false
		movingPiece.emit(false)
		get_tree().call_group("Space","grabPiece")

func _on_mouse_entered() -> void:
	isMouseEntered = true

func _on_mouse_exited() -> void:
	isMouseEntered = false
