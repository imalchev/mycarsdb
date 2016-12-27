export interface BadRequest {
    message: string;
    modelState: {
        errorMessages: Array<string>;
    }
}
