<?php

	error_reporting(E_ERROR | E_WARNING | E_PARSE | E_NOTICE);

	$host_name = $_POST["host_name"];
	$username = $_POST["username"];
	$password = $_POST["password"];
	$database_name = $_POST["database_name"];
	$port = $_POST["port"];
	$connection_failed_error_code = $_POST["connection_failed_error_code"];
	
	$table_name = $_POST["table_name"];
	$row_id = $_POST["row_id"];
	
	$delete_row_error_code = $_POST["delete_row_error_code"];
	
	
	
	$connection = mysqli_connect($host_name, $username, $password, $database_name, $port);
	
	if (mysqli_connect_errno())
	{
		die($connection_failed_error_code);
	}
	
	
	
	$delete_row_query = "DELETE FROM ". $table_name. " WHERE id=". $row_id. "";
	mysqli_query($connection, $delete_row_query) or die($delete_row_error_code);
	
	
	
	$connection->close();

?>