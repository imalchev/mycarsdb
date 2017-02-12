export const BASE_API_URI = 'http://localhost:49681/api';
export const SIMPLE_VALIDATION_EMAIL_PATTERN =
    `[A-Za-z0-9._%+-]{3,}@[a-zA-Z]{3,}([.]{1}[a-zA-Z]{2,}|[.]{1}[a-zA-Z]{2,}[.]{1}[a-zA-Z]{2,})`;
export const VALIDATION_EMAIL_REGEX: RegExp =
    /^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;

export const UNEXPECTED_RESPONSE = 'Unexpected response from server!';
export const UNAUTHORIZED = 'You are not authoarized!';
export const CHECK_INTERNET = 'Server is not accessible. Check you internet connection.';
export const INVALID_REQUEST_ADDRESS = 'Web adress is not valid';
