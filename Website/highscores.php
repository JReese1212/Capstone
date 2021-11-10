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

<h2>High Scores</h2>

<div class = "highscore">
<div class="table">
<?php
$sql = "SELECT username, highscore from highscore order by highscore DESC";
$result = $con->query($sql);

echo "<table>";
echo"<th></th>";
echo"<th>Username</th>";
echo"<th>Score</th>";
$i = 1;
if(mysqli_num_rows($result) > 0)
{
    while($row = $result->fetch_assoc()) {
    echo "<tr><td>" . $i . "</td>";
    echo "<td>" .$row['username'] ."</td><td>" .$row['highscore'] . "</td></tr>";
    ++$i;
    }
    
}

echo "</table>";

?>
</div>
</div>

<!--
<div class = "highscore">
<div class="table">

PHP STARTS

$query = "SELECT username, highscore from highscore order by highscore DESC";
$result = mysqli_query($con, $query);

echo "<table>";
echo"<th>Username</th>";
echo"<th>Score</th>";
while($row = mysqli_fetch_array($result)) {
    echo "<tr><td>" .$row['username'] ."</td><td>" .$row['highscore'] . "</td></tr>";
}
echo "</table>"

PHP ENDS

</div>
</div>

-->
</body>
</html>