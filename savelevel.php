<?php
$data=$_REQUEST["data"];

$myfile = fopen("level.xml", "w") or die("Unable to open file!");
fwrite($myfile, $data);
fclose($myfile);
?>
