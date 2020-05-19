<?php

	error_reporting(E_ERROR | E_WARNING | E_PARSE | E_NOTICE);

	$host_name = $_POST["host_name"];
	$username = $_POST["username"];
	$password = $_POST["password"];
	$database_name = $_POST["database_name"];
	$port = $_POST["port"];
	$connection_failed_error_code = $_POST["connection_failed_error_code"];
	
	$table_name = $_POST["table_name"];
	
	// $no_results_code = $_POST["no_results_code"];
	
	
	
	$connection = mysqli_connect($host_name, $username, $password, $database_name, $port);
	
	if (mysqli_connect_errno())
	{
		die($connection_failed_error_code);
	}
	
	
	
	$columns_array = array();
	
	$sql = "SHOW columns FROM ". $table_name. "";
	$result = mysqli_query($connection, $sql);
	
	$column_amount = mysqli_num_fields($result);
	echo($column_amount. "\t");
	
	$column_names = array();
	
	while ($row = mysqli_fetch_array($result))
	{
		array_push($column_names, $row['Field']);
		echo($row['Field']. "\t");
	}
	
	$result = $connection->query("SELECT * FROM ". $table_name. "");
	
	if ($result->num_rows > 0)
	{
		while ($row = $result->fetch_assoc())
		{
			for ($i = 0; $i < $column_amount; $i++)
			{
				echo($row[$column_names[$i]]. "\t");
			}
		}
	}
	else
	{
		echo $no_results_code;
	}
	
	$connection->close();

?>