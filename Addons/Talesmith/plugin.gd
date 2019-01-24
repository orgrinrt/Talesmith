tool
extends EditorPlugin

var dock;
var info_label : Label;
var timer : Timer;

func _enter_tree():
	add_custom_type(
	"ApplicationPrefs", 
	"Resource", 
	preload("ResourceTypes/PreferenceTypes/app_prefs.gd"), 
	preload("Icons/Application.png"));
	
	add_custom_type(
	"AppearancePrefs", 
	"Resource", 
	preload("ResourceTypes/PreferenceTypes/appearance_prefs.gd"), 
	preload("Icons/AppPrefs.png"));
	
	add_custom_type(
	"ViewPrefs", 
	"Resource", 
	preload("ResourceTypes/PreferenceTypes/view_prefs.gd"), 
	preload("Icons/ViewPrefs.png"));
	
	add_custom_type(
	"ThemeSet", 
	"Resource", 
	preload("ResourceTypes/theme_set.gd"), 
	preload("Icons/ThemeSet.png"));
	
	dock = preload("Dock/Dock.tscn").instance();
	add_control_to_dock(EditorPlugin.DOCK_SLOT_RIGHT_UR, dock);
	
	info_label = dock.get_node("HBoxContainer/SuccessLabel");
	timer = dock.get_node("Timer");
	
	dock.get_node("HBoxContainer/Button").connect("button_up", self, "on_button_pressed");
	info_label.hide();

func _exit_tree():
	#Preferences
	remove_custom_type("ApplicationPrefs");
	remove_custom_type("AppearancePrefs");
	remove_custom_type("ViewPrefs");
	#Resources
	remove_custom_type("ThemeSet");
	
	remove_control_from_docks(dock);
	dock.free();
	
func on_button_pressed():
	info_label.show();
	timer.set_wait_time(1);
	timer.start();
	timer.connect("timeout", self, "on_timer_end");
	
func on_timer_end():
	info_label.hide();
	timer.disconnect("timeout", self, "on_timer_end");