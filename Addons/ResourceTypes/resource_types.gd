tool
extends EditorPlugin

var dock;
var info_label : Label;
var timer : Timer;

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
	
	dock = preload("res://Addons/ResourceTypes/Dock/Dock.tscn").instance();
	add_control_to_dock(EditorPlugin.DOCK_SLOT_RIGHT_UR, dock);
	
	info_label = dock.get_node("HBoxContainer/SuccessLabel");
	timer = dock.get_node("Timer");
	
	dock.get_node("HBoxContainer/Button").connect("button_up", self, "on_button_pressed");
	info_label.hide();

func _exit_tree():
	remove_custom_type("AppPrefs");
	remove_custom_type("ViewPrefs");
	
	remove_control_from_docks(dock);
	dock.free();
	
func update_resources():
	
	remove_custom_type("AppPrefs");
	remove_custom_type("ViewPrefs");
	
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
	
func on_button_pressed():
	update_resources();
	info_label.show();
	timer.set_wait_time(1);
	timer.start();
	timer.connect("timeout", self, "on_timer_end");
	
func on_timer_end():
	info_label.hide();
	timer.disconnect("timeout", self, "on_timer_end");