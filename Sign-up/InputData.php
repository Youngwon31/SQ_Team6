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
}

// POST 요청에서 데이터 수집 및 정제
$formUsername = $db->real_escape_string($_POST['username']);
$formEmail = $db->real_escape_string($_POST['email']); // 이메일 추가
$formPassword = password_hash($_POST['password'], PASSWORD_DEFAULT);

// 데이터베이스에 사용자 정보 삽입
$query = "INSERT INTO users (username, email, password) VALUES ('$formUsername', '$formEmail', '$formPassword')";
if ($db->query($query) === TRUE) {
    // JSON 응답을 반환
    echo json_encode(['success' => true, 'message' => 'Signup successful']);
} else {
    // 오류 메시지를 JSON으로 반환
    echo json_encode(['success' => false, 'message' => "Error: " . $query . "<br>" . $db->error]);
}

// 데이터베이스 연결 종료
$db->close();
?>