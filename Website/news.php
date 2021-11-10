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
<title>News</title>
</head>
<body>


<div class="topnav">

<!-- Centered link -->
<div class="topnav-centered">
<a href="highscores.php">High Scores</a>
</div>
<!-- left aligned navs --> 
<a href="index.php">Home</a>
<a class="active" href="news.php">News</a>
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

<h2>News</h2>

<div class = "news">

<p>
"Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur? Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur?"
</p>
<p class = "date">
November 6, 2021
</p>

</div><br>

<div class = "news">

<p>
<h3>First update!</h3>
"Hello! We are Kent State University Students that are in our Capstone class and we are creating a game for our project. It is going to be a survival game where you
drive a car around and try to run from the cops. We have many ideas that we wish to implement and many updates to come!"
</p>
<p class = "date">
November 1, 2021
</p>

</div><br>


</body>

</html>