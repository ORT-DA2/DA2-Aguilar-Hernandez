import { Role } from './role';

export interface User {
  id: string;
  firstName: string;
  lastName: string;
  username: string;
  password: string;
  roles: Role[];
  email: string;
}
