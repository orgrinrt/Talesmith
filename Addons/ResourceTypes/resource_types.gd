tool
extends EditorPlugin

func _enter_tree():
	add_custom_type(
	"AppPrefs", 
	"Resource", 
	preload("AppPrefs.cs"), 
	preload("Icons/AppPrefs.png"));

func _exit_tree():
	remove_custom_type("AppPrefs");