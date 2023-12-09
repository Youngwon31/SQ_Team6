<?php

$servername = "127.0.0.1"; // 데이터베이스 서버 주소
$dbUsername = "root"; // MySQL 사용자 이름
$dbPassword = "0000"; // MySQL 비밀번호
$dbname = "UserDB"; // 데이터베이스 이름

// 데이터베이스에 연결
$db = new mysqli($servername, $dbUsername, $dbPassword, $dbname);

// 연결 확인
if ($db->connect_error) {
    die("Connection failed: " . $db->connect_error);
} else {
    echo "Connected successfully";
}

// POST 요청에서 데이터 수집 및 정제
// POST 요청에서 데이터 수집 및 정제
if (isset($_POST['username'], $_POST['email'], $_POST['password'])) {
    $formUsername = $db->real_escape_string(trim($_POST['username']));
    $formEmail = $db->real_escape_string(trim($_POST['email']));
    $formPassword = $db->real_escape_string(trim($_POST['password']));
    // 데이터베이스에 사용자 정보 삽입
    $query = "INSERT INTO users (username, email, password) VALUES ('$formUsername', '$formEmail', '$formPassword')";
    if ($db->query($query) === TRUE) {
        echo "Signup successful";
    } else {
        echo "Error: " . $query . "<br>" . $db->error;
    }
} else {
    echo "Missing required post data";
}


// 데이터베이스 연결 종료
$db->close();
?>