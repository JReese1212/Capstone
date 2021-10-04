<?php

session_start();

    include("connections.php");
    include("functions.php");

    if($_SERVER['REQUEST_METHOD'] == "POST")
    {
        //posted information
        $email = $_POST['email'];
        $pass = $_POST['pass'];
        if(!empty($email) && !empty($pass)) {
            //read from database
            $query = "select * from login where email = '$email'";
            $result = mysqli_query($con, $query);

            //if valid result, continue
            if($result)
            {
                //if theres only 1 result
                if($result && mysqli_num_rows($result) > 0)
                {
                    $userdata = mysqli_fetch_assoc($result);

                    if($userdata['pass'] === $pass)
                    {
                        $_SESSION['userid'] = $userdata['userid'];
                        header("Location: index.php");
                        die;
                    }
                }
            }
            //TODO make this pop up on screen
            echo "wrong Email or password";
        }else{
            echo "Invalid Email or Password ";
        }

    }
?>

<!DOCTYPE html>
<html lang="en" dir="ltr">
<head>
<meta charset = "utf-8">
<link type="text/css" rel="stylesheet" href="index.scss">
<title>Login</title>
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
<a href="contact.php">Contact Us</a>

<!-- right aligned navs -->
    <div class="topnav-right">
    <?php
    if(isset($_SESSION['userid']))
    {
        echo "<a href='logout.php'>Logout</a>";
    }else{
        echo "<a class='active' href='login.php'>Login</a>
        <a href='register.php'>Register</a>";
    }
    ?>
    </div>
</div>

<div class="box">
    <form method="POST">
        <div style="font-size: 24px; margin: 10px;">Login</div>
        <input class="text" type="email" id="email" placeholder="Email" name="email" required><br><br>
        <input class="text" type="password" id="pass" placeholder="Password" name="pass" required><br><br>

        <input id="button" type="submit" value="Login"><br><br>

        <div>Not registered? <a id="reg" href="register.php">Click here to start</a></div><br><br>
    </form>
</div>




</body>

</html>