<?php
session_start();

    include("connections.php");
    include("functions.php");

    if($_SERVER['REQUEST_METHOD'] == "POST")
    {
        //posted information
        $username = $_POST['username'];
        $email = $_POST['email'];
        $pass = $_POST['pass'];

        $username = stripslashes($username);
        $pass = stripslashes($pass);
        $username = mysqli_real_escape_string($con, $username);
        $pass = mysqli_real_escape_string($con, $pass);

        if($stmt = $con->prepare('SELECT email FROM login WHERE email = ?'))
        {
            $stmt->bind_param('s', $_POST['email']);
            $stmt->execute();
            $stmt->store_result();
            if($stmt->num_rows > 0)
            {
                echo "<script>alert('Email already exists! Please enter a different email')</script>";
            }
            else
            {
                $stmt = $con->prepare('INSERT INTO login (userid, username, email, pass)
                values (?, ?, ?, ?)');
                $userid = randomNumber(10);  //hash for password
                $hashed_pass = password_hash($pass, PASSWORD_BCRYPT);
                $stmt->bind_param('isss', $userid, $_POST['username'], $_POST['email'], $hashed_pass);
                $stmt->execute();

                header("Location: login.php");
                die();
            }
        }else
        {
            echo "Please try again!";
        }

    }


?>


<!--<php

====OLD PHP====

session_start();

    include("connections.php");
    include("functions.php");

    if($_SERVER['REQUEST_METHOD'] == "POST")
    {
        //posted information
        $username = $_POST['username'];
        $email = $_POST['email'];
        $pass = $_POST['pass'];

        $username = stripslashes($username);
        $pass = stripslashes($pass);
        $username = mysqli_real_escape_string($con, $username);
        $pass = mysqli_real_escape_string($con, $pass);

        if(!empty($username) && !empty($pass) && 
        !empty($email) && !is_numeric($username))
        {
            //save to database
            $userid = randomNumber(10);  //hash for password

            $hashed_pass = password_hash($pass, PASSWORD_BCRYPT);

            $query = "insert into login (userid, username, email, pass)
            values ('$userid', '$username', '$email', '$hashed_pass')";

            mysqli_query($con, $query);

            header("Location: login.php");
            die;


        }else
        {
            echo "Please enter some valid information!";
        }

    }


?>-->

<!DOCTYPE html>
<html lang="en" dir="ltr">
<head>
<meta charset = "utf-8">
<link type="text/css" rel="stylesheet" href="index.scss">
<title>Register</title>
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
        echo "<a href='login.php'>Login</a>
        <a class='active' href='register.php'>Register</a>";
    }
    ?>
    </div>
</div>

<div class="box">
    <form method="POST">
        <div style="font-size: 24px; margin: 10px;">Register</div>
        <input class="text" type="email" id="email" placeholder="Email" name="email" required><br><br>
        <input class="text" type="text" id="username" placeholder="User Name" name="username" pattern="[A-Za-z0-9_-]{3,16}"
        title="Letters, numbers, dashes, and underscores only. Please try again without symbols." required><br><br>
        <input class="text" type="password" id="pass" placeholder="Password" name="pass" pattern="(?=.*\d)(?=.*[a-z]).{8,}"
        title="Must contain at least one number and one letter, and at least 8 or more characters" required><br><br>
        <input class="text" type="password" id="confirm_pass" placeholder="Confirm Password" name="confirm_pass" pattern="(?=.*\d)(?=.*[a-z]).{8,}"
        title="Must contain at least one number and one letter, and at least 8 or more characters" required><br><br>

        <input id="button" type="submit" value="Register"><br><br>
        <div>Already registered? <a id="reg" href="login.php">Click here to Log in</a></div><br><br>
    </form>
</div>

<script type="text/javascript">
var password = document.getElementById("pass")
  , confirm_password = document.getElementById("confirm_pass");

function validatePassword(){
  if(password.value != confirm_password.value) {
    confirm_password.setCustomValidity("Passwords Don't Match");
  } else {
    confirm_password.setCustomValidity('');
  }
}

password.onchange = validatePassword;
confirm_password.onkeyup = validatePassword;
</script>


</body>

</html>