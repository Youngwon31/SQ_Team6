<?php
$servername = "localhost"; // 데이터베이스 서버 주소
$username = "root"; // MySQL 사용자 이름
$password = "0000"; // MySQL 비밀번호
$dbname = "UserDB"; // 데이터베이스 이름

// MySQL에 연결
$conn = new mysqli($servername, $username, $password, $dbname);

// 연결 확인
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

// POST 요청 데이터 확인
if ($_SERVER["REQUEST_METHOD"] == "POST") {
    // 입력 데이터 받아오기
    $new_username = $conn->real_escape_string($_POST['username']);
    $new_email = $conn->real_escape_string($_POST['email']);
    $new_password = $conn->real_escape_string($_POST['password']);

    // 비밀번호 해싱 (보안을 위해)
    $hashed_password = password_hash($new_password, PASSWORD_DEFAULT);

    // 쿼리 준비
    $sql = "INSERT INTO users (username, email, password) VALUES ('$new_username', '$new_email', '$hashed_password')";

    // 쿼리 실행
    if ($conn->query($sql) === TRUE) {
        echo "New record created successfully";
    } else {
        echo "Error: " . $sql . "<br>" . $conn->error;
    }
}

// 연결 종료
$conn->close();
?>