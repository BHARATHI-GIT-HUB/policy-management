import { city } from './city';
import { user } from './user';

export interface Agent {
  id: number;
  dob: string;
  street: string;
  cityId: number;
  mobileNo: string;
  qualification: string;
  aadharNo: number;
  panNo: number;
  userId: number;

  city: city;
  user: user;
}
