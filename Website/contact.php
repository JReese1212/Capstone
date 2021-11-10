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
<title>Contact</title>
</head>
<body>

<div class="topnav">

<!-- Centered link -->
<div class="topnav-centered">
<a href="highscores.php">High Scores</a>
</div>
<!-- left aligned navs --> 
<a href="index.php">Home</a>
<a href="news.php">News</a>
<a class="active" href="contact.php">Contact Us</a>

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

<h2>Contact us</h2>

<div class = "contact">

<p>
"Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur? Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur?"
</p>

</div>




</body>

</html>