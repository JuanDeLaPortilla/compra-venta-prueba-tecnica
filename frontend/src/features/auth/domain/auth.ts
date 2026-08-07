import type { ApiResponse } from "@/core/http/api-response";

export interface LoginRequest {
  username: string;
  password: string;
}

export interface LoginResponse extends ApiResponse<null> {
  token: string;
}

export interface JwtClaims {
  userId?: string;
  username?: string;
  exp?: number;
  iat?: number;
}