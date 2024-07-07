import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { ApiClientService } from './api-client.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { SysRoles } from '../enums/sys-roles';

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  private accessToken: string | null = null;

  constructor(private apiClientService: ApiClientService) {
    this.accessToken = localStorage.getItem('accessToken');

  }

  login(username: string, password: string) {
    return this.apiClientService.post<any>(`/api/login`, { username, password })
      .pipe(map(response => {
        this.setAccessToken(response.token);
        return response.user;
      }));
  }

  logout() {
    localStorage.removeItem('accessToken');
    this.accessToken = null;
  }

  getAccessToken(): string | null {
    return this.accessToken;
  }

  setAccessToken(token: string): void {
    this.accessToken = token;
    localStorage.setItem('accessToken', token);
  }

  get accessTokenClaims() {
    if (this.accessToken == null)
        return null;

    let jwtHelper: JwtHelperService = new JwtHelperService();
    const accessTokenClaims = jwtHelper.decodeToken(this.accessToken);
    return accessTokenClaims;
  }
  get userRolesFromAccessToken() {
    return this.accessTokenClaims.realm_access.roles;
  }
  get isLoggedIn()
  {
    return this.accessToken != null;
  }
  get isTokenExpired() {
    const decodedToken = this.accessTokenClaims();
    if (decodedToken) {
      return Date.now() >= decodedToken.exp * 1000;
    }
    return true;
  }
  isInRoles(roles: string[]): boolean {
    if (this.userRolesFromAccessToken.includes(SysRoles.Admin))
      return true;

    else if (!roles || roles.length === 0)
      return false;
    return roles.some((r) => this.userRolesFromAccessToken.includes(r));
  }
}
