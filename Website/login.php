<!DOCTYPE html>
<html lang="en" dir="ltr">
<head>
<meta charset = "utf-8">
<link type="text/css" rel="stylesheet" href="index.scss">
<title>Login</title>
</head>
<body>

<?php

//TODO figure out login page still;
//time in video 33~
session_start();

    include("connections.php");
    include("functions.php");

    if($_SERVER['REQUEST_METHOD'] == "POST")
    {
        //posted information
        $email = $_POST['email'];
        $pass = $_POST['pass'];

        if(!empty($username) && !empty($pass) && !is_numeric($username)) {
            //read from database
            $query = "select * from login where username = $username";
            
            $result = mysqli_query($con, $query);

            if($result)
            {
                if($result && mysqli_num_rows($result) > 0)
                {
                    $userdata = mysqli_fetch_assoc($result);

                    if($userdata['pass'] == $pass)
                    {
                        $_SESSION['userid'] = $userdata['userid'];
                        header("Location: index.php");
                        die;
                    }
                }
            }
        }else{
            echo "Wrong ";
        }

    }
?>
<!-- top navigation -->
<div class="topnav">

<a href="index.php">Home</a>
<a href="news.php">News</a>
<a href="contact.php">Contact</a>
<a href="about.php">About</a>
<!-- right aligned navs -->
<div class="topnav-right">
    <a class="active" href="login.php">Login</a>
    <a href="register.php">Register</a>
</div>

</div>

<div class="box">
    <form method="POST">
        <div style="font-size: 24px; margin: 10px;">Login</div>
        <input class="text" type="email" placeholder="Email" name="email"><br><br>
        <input class="text" type="password" placeholder="Password" name="password"><br><br>

        <input id="button" type="submit" value="Login"><br><br>

        <div>Not registered? <a id="reg" href="register.php">Click here to start</a></div><br><br>
    </form>
</div>




</body>

</html>