<?php

	$host_name = $_POST["host_name"];
	$username = $_POST["username"];
	$password = $_POST["password"];
	$database_name = $_POST["database_name"];
	$port = $_POST["port"];
	$connection_failed_error_code = $_POST["connection_failed_error_code"];
	
	$table_name = $_POST["table_name"];
	$columns_amount = $_POST["columns_amount"];
	$columns_names = $_POST["columns_names"];
	$data_to_insert = $_POST["data_to_insert"];
	
	$command = $_POST["command"];
	
	$insert_row_failed_code = $_POST["insert_row_failed_code"];
	
	
	
	$connection = mysqli_connect($host_name, $username, $password, $database_name, $port);
	
	if (mysqli_connect_errno())
	{
		die($connection_failed_error_code);
	}
	
	
	
	mysqli_query($connection, $command) or die($insert_row_failed_code);
	
	
	
	$connection->close();

?>