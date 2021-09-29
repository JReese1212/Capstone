<?php

include("connections.php");

function check_login($con)
{

    if(isset($_SESSION['userid']))
    {
        $id = $_SESSION['userid'];
        $query = "select * from users where userid = '$id' limit 1";

        $result = mysqli_query($con, $query);
        if($result && mysqli_num_rows($result) > 0)
        {
            $userdata = mysqli_fetch_assoc($result);
            return $userdata;
        }
    }

}

function randomNumber($length) {
    $result = '';

    for($i = 0; $i < $length; $i++) {
        $result .= mt_rand(0, 9);
    }

    return $result + 1;
}