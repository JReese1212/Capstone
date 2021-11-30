<?php
session_start();

    include("connections.php");
    include("functions.php");
    include("apiFunc.php");

    if(!isset($_SESSION['userid']))
    {
        header("Location: login.php");
        die();
    }
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

<!-- left aligned navs -->
<a href="index.php">Home</a>
<a href="highscores.php">Scores</a>
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

<h2>Scores for <?php echo $userdata['username']; ?></h2>

<div class = "highscore">
<div class="table">
<?php

if(isset($_SESSION))
{

        $userid = $_SESSION['userid'];
    $get_data = callAPI('GET', 'http://3.22.125.44:8000/api/Scores/' . $userid, false);
    $response = json_decode($get_data, true);


    if($response)
    {
        echo "<table>";
        echo"<th>Rankings</th>";
        echo"<th>Scores</th>";
        echo "<tr><td>1</td><td>" .$response[highscore] ."</td></tr>";
        echo "<tr><td>2</td><td>" .$response[score2] ."</td></tr>";
        echo "<tr><td>3</td><td>" .$response[score3] ."</td></tr>";
        echo "<tr><td>4</td><td>" .$response[score4] ."</td></tr>";
        echo "<tr><td>5</td><td>" .$response[score5] ."</td></tr>";
        echo "<tr><td>6</td><td>" .$response[score6] ."</td></tr>";
        echo "<tr><td>7</td><td>" .$response[score7] ."</td></tr>";
        echo "<tr><td>8</td><td>" .$response[score8] ."</td></tr>";
        echo "<tr><td>9</td><td>" .$response[score9] ."</td></tr>";
        echo "<tr><td>10</td><td>" .$response[score10] ."</td></tr>";
    }

    echo "</table>";

}

else{
    header("Location: login.php");
    die();
}

?>
</div>
</div>

</body>
</html>