import { city } from './city';
import { user } from './user';
export interface Client {
  id: number;
  name: string;
  dob: Date;
  mobileNo: string;
  mailId: string;
  fatherName: string;
  motherName: string;
  nationality: string;
  street: string;
  cityId: number;
  userId: number;
  user: user;
  city: city;
}
