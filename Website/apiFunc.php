<?php

  function callAPI($method, $url, $data) {
          $curl = curl_init();
          switch($method) {
          case "POST":
                  curl_setopt($curl, CURLOPT_POST, 1);
                  if($data)
                          curl_setopt($curl, CURLOPT_POSTFIELDS, $data);
                  break;
          case "PUT":
                  curl_setopt($curl, CURLOPT_CUSTOMREQUEST, "PUT");
                  if($data)
                          curl_setopt($curl, CURLOPT_POSTFIELDS, $data);
                  break;
          default:
                  if($data)
                          $url = sprintf("%s?%s", $url, http_build_query($data));
          }
          curl_setopt($curl, CURLOPT_URL, $url);
          curl_setopt($curl, CURLOPT_PORT, 8000);
          curl_setopt($curl, CURLOPT_HTTPHEADER, array(
                  'Content-Type: application/json',
          ));
          curl_setopt($curl, CURLOPT_RETURNTRANSFER, 1);
          curl_setopt($curl, CURLOPT_HTTPAUTH, CURLAUTH_ANY);
          curl_setopt($curl, CURLOPT_FAILONERROR, true);

          $result = curl_exec($curl);
          $http_status = curl_getinfo($curl, CURLINFO_HTTP_CODE);
          if(curl_errno($curl)){
                  echo 'Curl error: ' . curl_error($curl);
          }
          if(!$result){
                  $result = $http_status;
          }
          curl_close($curl);
          return $result;
  }



?>