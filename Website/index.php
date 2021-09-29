<!DOCTYPE html>
<html lang="en" dir="ltr">
<head>
<meta charset = "utf-8">
<link type="text/css" rel="stylesheet" href="index.scss">
<title>car survival game</title>
</head>
<body>

<?php
session_start();

    include("connections.php");
    include("functions.php");

    $user_data = check_login($con);


?>


<div class="topnav">
<!-- left aligned navs --> 
<a class="active" href="index.php">Home</a>
<a href="news.php">News</a>
<a href="contact.php">Contact</a>
<a href="about.php">About</a>

<!-- right aligned navs -->
    <div class="topnav-right">
    <a href="login.php">Login</a>
    <a href="register.php">Register</a>
    </div>
</div>

<!-- video 
<div class="video">
<video controls> 
<source src="tik.mp4" type="video/mp4">
Your browser does not support the video tag.
</video>
</div>-->

<!-- car images -->
<div class = "cars">
<img src="car1.jpg" alt="Car" class="carcol">
<img src="car2.jpg" alt="Car" class="carcol">
<img src="car3.jpg" alt="Car" class="carcol">
</div>

</body>
</html>