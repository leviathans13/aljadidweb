#!/bin/bash

BASE_URL="http://localhost:5000"

echo "=== Testing Surat Menyurat API ==="
echo

echo "1. Testing API Health (should return Swagger page)"
curl -s -o /dev/null -w "HTTP Status: %{http_code}\n" "$BASE_URL"
echo

echo "2. Testing Auth - Login with default admin user"
LOGIN_RESPONSE=$(curl -s -X POST "$BASE_URL/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "admin@suratmenyurat.com",
    "password": "Admin123!"
  }')

if echo "$LOGIN_RESPONSE" | grep -q "token"; then
    echo "✓ Admin login successful"
    TOKEN=$(echo "$LOGIN_RESPONSE" | sed -n 's/.*"token":"\([^"]*\)".*/\1/p')
    echo "Token: ${TOKEN:0:50}..."
else
    echo "✗ Admin login failed"
    echo "Response: $LOGIN_RESPONSE"
fi
echo

echo "3. Testing Auth - Login with default user"
USER_LOGIN_RESPONSE=$(curl -s -X POST "$BASE_URL/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "user@suratmenyurat.com",
    "password": "User123!"
  }')

if echo "$USER_LOGIN_RESPONSE" | grep -q "token"; then
    echo "✓ User login successful"
    USER_TOKEN=$(echo "$USER_LOGIN_RESPONSE" | sed -n 's/.*"token":"\([^"]*\)".*/\1/p')
    echo "User Token: ${USER_TOKEN:0:50}..."
else
    echo "✗ User login failed"
    echo "Response: $USER_LOGIN_RESPONSE"
fi
echo

if [ ! -z "$TOKEN" ]; then
    echo "4. Testing Get Current User Info"
    curl -s -X GET "$BASE_URL/api/auth/me" \
      -H "Authorization: Bearer $TOKEN" | head -n 3
    echo
    echo

    echo "5. Testing Get Letters (should return empty list initially)"
    curl -s -X GET "$BASE_URL/api/letters" \
      -H "Authorization: Bearer $TOKEN" | head -n 3
    echo
    echo

    echo "6. Testing Get Users List"
    curl -s -X GET "$BASE_URL/api/letters/users" \
      -H "Authorization: Bearer $TOKEN" | head -n 3
    echo
    echo

    if [ ! -z "$USER_TOKEN" ]; then
        echo "7. Testing Create Letter"
        # Get recipient ID (should be the regular user)
        USERS_RESPONSE=$(curl -s -X GET "$BASE_URL/api/letters/users" -H "Authorization: Bearer $TOKEN")
        RECIPIENT_ID=$(echo "$USERS_RESPONSE" | sed -n 's/.*"id":"\([^"]*\)".*"email":"user@suratmenyurat.com".*/\1/p')
        
        if [ ! -z "$RECIPIENT_ID" ]; then
            CREATE_RESPONSE=$(curl -s -X POST "$BASE_URL/api/letters" \
              -H "Authorization: Bearer $TOKEN" \
              -H "Content-Type: application/json" \
              -d "{
                \"subject\": \"Test Letter\",
                \"content\": \"This is a test letter created via API\",
                \"type\": 1,
                \"recipientId\": \"$RECIPIENT_ID\",
                \"priority\": 2
              }")
            
            if echo "$CREATE_RESPONSE" | grep -q "Test Letter"; then
                echo "✓ Letter creation successful"
                LETTER_ID=$(echo "$CREATE_RESPONSE" | sed -n 's/.*"id":\([0-9]*\).*/\1/p')
                echo "Created Letter ID: $LETTER_ID"
            else
                echo "✗ Letter creation failed"
                echo "Response: $CREATE_RESPONSE"
            fi
        else
            echo "✗ Could not find recipient user ID"
        fi
    fi
    echo

    echo "8. Testing Admin Endpoints"
    curl -s -X GET "$BASE_URL/api/admin/statistics" \
      -H "Authorization: Bearer $TOKEN" | head -n 5
    echo
fi

echo
echo "=== Test Complete ==="