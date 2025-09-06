extends Node2D

@onready var animation_player: AnimationPlayer = $AnimationPlayer

func playAnimation(): 
	animation_player.play("Transition")
