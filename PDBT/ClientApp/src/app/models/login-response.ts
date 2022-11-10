export interface LoginResponse {
  token: string;
  jwt: string;
  userId: string;
  expires: Date;
}
