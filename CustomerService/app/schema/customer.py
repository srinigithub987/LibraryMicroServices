from pydantic import BaseModel, EmailStr


class CustomerRegister(BaseModel):

    first_name: str

    last_name: str

    email: EmailStr

    password: str


class CustomerLogin(BaseModel):

    email: EmailStr

    password: str


class TokenResponse(BaseModel):

    access_token: str

    token_type: str


class CustomerResponse(BaseModel):

    id: int

    first_name: str

    last_name: str

    email: EmailStr

    is_active: bool

    class Config:
        from_attributes = True