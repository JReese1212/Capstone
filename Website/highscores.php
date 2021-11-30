<?php
session_start();

    //include("connections.php");
    include("functions.php");
    include("apiFunc.php");

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
<a class="active" href="highscores.php">Scores</a>
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

<h2>Scores</h2>

<div class = "highscore">
<div class="table">
<?php

$get_data = callAPI('GET', 'http://3.22.125.44:8000/api/Scores/', false);
$response = json_decode($get_data, true);


echo "<table>";
echo"<th>Rankings</th>";
echo"<th>Username</th>";
echo"<th>Score</th>";
$i = 1;

if(count($response) > 0)
{
        $columns = array_column($response, 'highscore');
        array_multisort($columns, SORT_DESC, $response);


    for($x = 0; $x < count($response); $x++)
    {
            if($response[$x][highscore] > 0){
                    echo "<tr><td>" . $i . "</td>";
                    echo "<td>" . $response[$x][username] . "</td><td>" . $response[$x][highscore] . "</td></tr>";
            }
        ++$i;
    }
}


echo "</table>";

?>
</div>
</div><br>



<div class="scoresbox">
    <a href="personalScores.php">Check your scores!</a>
</div>

</body>
</html>
