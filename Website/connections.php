<?php

$dbhost = "localhost";
$dbuser = "root";
$dbpass = "";
$dbname = "car_survivor_game";

$con = new mysqli($dbhost, $dbuser, $dbpass, $dbname);
if(mysqli_connect_errno()) {
    die("Connection failed: " .mysqli_connect_error());
}