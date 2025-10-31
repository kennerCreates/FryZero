extends Area2D

var isMouseEntered = false
var isDragging = false
var delay = 10

func _physics_process(delta: float):
	if isDragging == true:
		var tween = get_tree().create_tween()
		tween.tween_property(self, "position", get_global_mouse_position(), delay * delta)

func _input(event):
	if event is InputEventMouseButton and event.button_index == MOUSE_BUTTON_LEFT:
			if event.pressed and isMouseEntered:
				isDragging = true
			else: 
				isDragging = false


func _on_mouse_entered() -> void:
	isMouseEntered = true


func _on_mouse_exited() -> void:
	isMouseEntered = false
