import { useMutation } from "@tanstack/react-query";
import { authService } from "../infrastructure/auth-service";
import { tokenStorage } from "@/core/storage/token-storage";

export const useLogin = () => {
  return useMutation({
    mutationFn: authService.login,

    onSuccess: (data) => {
      tokenStorage.set(data.token);
    },
  });
};