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
<a class="active" href="highscores.php">High Scores</a>
</div>
<!-- left aligned navs --> 
<a href="index.php">Home</a>
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


</body>

</html>