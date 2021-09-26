<!DOCTYPE html>
<html lang="en" dir="ltr">
<head>
<meta charset = "utf-8">
<link type="text/css" rel="stylesheet" href="index.scss">
<title>Register</title>

<script type="text/javascript">
var password = document.getElementById("password")
  , confirm_password = document.getElementById("confirm_password");

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

</head>
<body>



<?php

?>
<!-- top navigation -->
<div class="topnav">
<a href="index.php">Home</a>
<a href="news.php">News</a>
<a href="contact.php">Contact</a>
<a href="about.php">About</a>
<!-- right aligned navs -->
<div class="topnav-right">
    <a href="login.php">Login</a>
    <a class="active" href="register.php">Register</a>
</div>
</div>

<div class="box">
    <form method="POST">
        <div style="font-size: 24px; margin: 10px;">Register</div>
        <input class="text" type="text" placeholder="Email" name="email" required><br><br>
        <input class="text" type="text" placeholder="User Name" name="username" required><br><br>
        <input class="text" id="password" type="password" placeholder="Password" name="password" pattern="(?=.*\d)(?=.*[a-z]).{8,}"
        title="Must contain at least one number nad one letter, and at least 8 or more characters" required><br><br>
        <input onkeyup='validatePassword();' class="text" id="confirm_password" type="password" placeholder="Confirm Password" name="confirm_password" pattern="(?=.*\d)(?=.*[a-z]).{8,}"
        title="Must contain at least one number nad one letter, and at least 8 or more characters" required><br><br>

        <input onkeyup='validatePassword();' id="button" type="submit" value="Register"><br><br>
    </form>
</div>




</body>

</html>