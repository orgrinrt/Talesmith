tool
extends EditorPlugin

func _enter_tree():
	add_custom_type(
	"AppPrefs", 
	"Resource", 
	preload("app_prefs.gd"), 
	preload("Icons/AppPrefs.png"));
	
	add_custom_type(
	"ViewPrefs", 
	"Resource", 
	preload("view_prefs.gd"), 
	preload("Icons/ViewPrefs.png"));

func _exit_tree():
	remove_custom_type("AppPrefs");
	remove_custom_type("ViewPrefs");