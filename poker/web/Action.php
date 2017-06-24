<?php
include "Command.php";
	class Action{
		
		//security
		public $key = "fvggtzYH675PiXpjK5fGuGhadAa5Sjb1G4hUQobzlls=";
		public $iv = "2EPvpwkqNxcc4qmKlPv80cpNWuVu6ypjwhGGE5dceMI=";
		//

		
		function Action(){
		}

		function Login($username, $password){
			$service_port = 5555;
			$address = "yakir.ddns.net";
			$socket = socket_create(AF_INET, SOCK_STREAM, SOL_TCP);
			if ($socket === false) {
				echo "socket_create() failed: reason: " . socket_strerror(socket_last_error()) . "</br>";
				die();
			}
			$result = socket_connect($socket, $address, $service_port);
			if ($result === false) {
				echo "socket_connect() failed.\nReason: ($result) " . socket_strerror(socket_last_error($socket)) . "</br>";
				die();
			}
			$command = new Command("LoginWeb", array($username,$password));
			$out = json_encode($command);
			$out .= "\n";
			//security
			$out = $this->encryptRJ256($this->key,$this->iv,$out);
			//
			$this->sendMsg($out,$socket);
			$in = '';
		
			while($resp = socket_read($socket, 1000)) {
			   $in .= $resp;
			   if (strpos($in, "\n") !== false) break;
			}
			
			//security
			$in = $this->decryptRJ256($this->key,$this->iv,$in);
			//
			$in = trim(preg_replace("/\xEF\xBB\xBF/", "", $in));
			socket_close($socket);	
			if($in == "Error"){
				session_unset();
				return false;
			}
			$in = json_decode($in); //in[0] myPlayer info , in[1] top by Gross profit, in[2] top by Highest gain , in[3] top by Number of games
			$_SESSION["username"] = $username;
			$_SESSION["info"] = $in;
			
			return true;
		}
		
		function sendMsg($st,$socket){
			$length = strlen($st);     
			while (true) {			
				$sent = socket_write($socket, $st, $length);					
				if ($sent === false) {	
					echo "sent false";
					break;
				}					
				// Check if the entire message has been sented
				if ($sent < $length) {
						
					// If not sent the entire message.
					// Get the part of the message that has not yet been sented as message
					$st = substr($st, $sent);
						
					// Get the length of the not sented part
					$length -= $sent;
				} else {
					echo "length false";					
					break;
				}
			}
			socket_write ($socket, "\r\n", strlen ("\r\n"));
		}
		
		//security - not used yet
		
		function decryptRJ256($key,$iv,$encrypted)
		{
			//PHP strips "+" and replaces with " ", but we need "+" so add it back in...
			$encrypted = str_replace(' ', '+', $encrypted);

			//get all the bits
			$key = base64_decode($key);
			$iv = base64_decode($iv);
			$encrypted = base64_decode($encrypted);

			$rtn = mcrypt_decrypt(MCRYPT_RIJNDAEL_256, $key, $encrypted, MCRYPT_MODE_CBC, $iv);
			$rtn = $this->unpad($rtn);
			return($rtn);
		}
		
		function encryptRJ256($key,$iv,$decrypted)
		{
			//PHP strips "+" and replaces with " ", but we need "+" so add it back in...
			//$decrypted = str_replace(' ', '+', $decrypted);
			
			//TODO: need to add padding
			
			//get all the bits
			$key = base64_decode($key);
			$iv = base64_decode($iv);
			$decrypted = $this->pkcs7pad($decrypted);
			$rtn = mcrypt_encrypt(MCRYPT_RIJNDAEL_256, $key, $decrypted, MCRYPT_MODE_CBC, $iv);			
			$rtn = base64_encode($rtn);
			return($rtn);
		}

		//removes PKCS7 padding
		function unpad($value)
		{
			$blockSize = mcrypt_get_block_size(MCRYPT_RIJNDAEL_256, MCRYPT_MODE_CBC);
			$packing = ord($value[strlen($value) - 1]);
			if($packing && $packing < $blockSize)
			{
				for($P = strlen($value) - 1; $P >= strlen($value) - $packing; $P--)
				{
					if(ord($value{$P}) != $packing)
					{
						$packing = 0;
					}
				}
			}

			return substr($value, 0, strlen($value) - $packing); 
		}
		
		function pkcs7pad($plaintext)
		{
			$blockSize = mcrypt_get_block_size(MCRYPT_RIJNDAEL_256, MCRYPT_MODE_CBC);
			$padsize = $blockSize - (strlen($plaintext) % $blockSize);
			return $plaintext . str_repeat(chr($padsize), $padsize);
		}
		
	}


?>