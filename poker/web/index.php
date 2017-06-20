<?php
ini_set('session.cookie_domain', '');
session_start();
session_unset();
$loginFail = false;
include_once("Action.php");

if(isset($_POST["uname"]) && isset($_POST["psw"])){
	$action = new Action();	
	$username = $_POST["uname"];
	$password = $_POST["psw"];
	$loginFail = !$action->login($username, $password);
	if(!$loginFail){
		header("location: home.php"); 
		exit();
	}
}

?>


<!DOCTYPE html>
<html>
<style>
body{
	background-image: url("images/MainBackground.jpg");
	align:center;
	color : white;

}


h1{
	text-align : center;
	color : white;
}

label {
	color : white;
	display: block;
}

form {
    border: 2px solid #f1f1f1;
	width : 50%;
	margin : auto;
}

input[type=text], input[type=password] {
    width: 100%;
    padding: 12px 20px;
    margin: 8px 0;
    display: inline-block;
    border: 1px solid #ccc;
    box-sizing: border-box;
}

button {
    background-color: #4CAF50;
    color: white;
    padding: 14px 20px;
    margin: 8px 0;
    border: none;
    cursor: pointer;
    width: 100%;
}

button:hover {
    opacity: 0.8;
}

.container {
    padding: 16px;
}
.errorLine {
	text-align : center;
	color : red;
	font-weight : bold;
}
</style>
<body>

<h1>Homey Poker Game</h1>

<form action="/index.php" method="post">

  <div class="container">
  
	
    <label><b>Username</b></label>
    <input type="text" placeholder="Enter Username" name="uname" onClick="hideErrorP()" required>

    <label><b>Password</b></label>
    <input type="password" placeholder="Enter Password" name="psw" onClick="hideErrorP()" required>
        
    <button type="submit">Login</button>
  </div>
  

  
</form>


<p id="errorLine" class="errorLine"> </p>

<script>
var loginFail = "<?php echo $loginFail?>";
function hideErrorP(){
	document.getElementById("errorLine").innerHTML="";

}
if(loginFail)
 document.getElementById("errorLine").innerHTML="Error With Login , Please Try Again";

</script>



</body>
</html>