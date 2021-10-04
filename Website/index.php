<?php
session_start();

    include("connections.php");
    include("functions.php");

    $userdata = check_login($con);

?>


<!DOCTYPE html>
<html lang="en" dir="ltr">
<head>
<meta charset = "utf-8">
<link type="text/css" rel="stylesheet" href="index.scss">
<title>car survival game</title>
</head>
<body>


<div class="topnav">

<!-- Centered link -->
<div class="topnav-centered">
<a href="highscores.php">High Scores</a>
</div>
<!-- left aligned navs --> 
<a class="active" href="index.php">Home</a>
<a href="news.php">News</a>
<a href="contact.php">Contact Us</a>

<!-- right aligned navs -->
    <div class="topnav-right">
    <?php
    if(isset($_SESSION['userid']))
    {
        echo "<a href='logout.php'>Logout</a>";
    }else{
        echo "<a href='login.php'>Login</a>
        <a href='register.php'>Register</a>";
    }
    ?>
    </div>
</div>

<div class="container">

<img src="backgroundcar.jpg" alt="Car">
<button class="btn" onclick="window.location.href='download.php'">Download</button>

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
<div class="cartext">
<h2>Car 1</h2>
<img src="car1.jpg" alt="Car" class="carcol">
<p>car information</p>
</div>
<div class="cartext">
<h2>Car 2</h2>
<img src="car2.jpg" alt="Car" class="carcol">
<p>car information</p>
</div>
<div class="cartext">
<h2>Car 3</h2>
<img src="car3.jpg" alt="Car" class="carcol">
<p>car information</p>
</div>
</div>

</body>
</html>