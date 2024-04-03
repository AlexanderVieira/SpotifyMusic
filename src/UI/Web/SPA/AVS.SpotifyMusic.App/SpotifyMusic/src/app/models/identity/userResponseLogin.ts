import { Guid } from "guid-typescript";
import { UserToken } from "./userToken";

export class UserResponseLogin {

  accessToken!: string;
  refreshToken!: Guid;
  expiresIn!: number;
  userToken!: UserToken;

}
