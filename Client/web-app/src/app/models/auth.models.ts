export interface SuccessLoginResponseModel {
    access_token: string;
    token_type: string;
    expires_in: number;
}

export interface ErrorLoginResponseModel {
    error: string;
    error_description: string;
}

export interface LoginResponseModel extends SuccessLoginResponseModel, ErrorLoginResponseModel {

}

export interface RegisterUserModel {
    email: string;
    password: string;
    confirmPassword: string;
    name: string;
    phoneNumber: string;
}
