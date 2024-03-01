export interface Agent {
  id: number;
  userId?: number;
  dob: string;
  street: string;
  cityId: number;
  mobileNo: string;
  qualification: string;
  aadharNo: number;
  panNo: number;
  // city: City;
  // incentives: Incentive[];
  // policyEnrollments: PolicyEnrollment[];
  // policyHistories: PolicyHistory[];
  // user?: User | null;
}
