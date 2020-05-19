<?php

	$host_name = $_POST["host_name"];
	$username = $_POST["username"];
	$password = $_POST["password"];
	$database_name = $_POST["database_name"];
	$port = $_POST["port"];
	$connection_failed_error_code = $_POST["connection_failed_error_code"];
	
	$table_name = $_POST["table_name"];
	$column_name = $_POST["column_name"];
	$new_data = $_POST["new_data"];
	$row_id = $_POST["row_id"];
	
	$update_table_error_code = $_POST["update_table_error_code"];
	
	
	
	$connection = mysqli_connect($host_name, $username, $password, $database_name, $port);
	
	if (mysqli_connect_errno())
	{
		die($connection_failed_error_code);
	}



	$update_table_query = "UPDATE ". $table_name. " SET ". $column_name. "='". $new_data. "' WHERE id=". $row_id. ";";
	mysqli_query($connection, $update_table_query) or die($update_table_error_code);

?>