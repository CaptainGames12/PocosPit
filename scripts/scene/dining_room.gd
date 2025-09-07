extends Node2D

@onready var color_rect: ColorRect = $ColorRect

func ballPool(_var1 : int):
	color_rect.z_index = _var1
