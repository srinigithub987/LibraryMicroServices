from app.models.customer import Customer
from app.core.security import (
    hash_password,
    verify_password,
    create_access_token
)


def register_customer(
    db,
    first_name: str,
    last_name: str,
    email: str,
    password: str
):

    existing_customer = (
        db.query(Customer)
        .filter(Customer.email == email)
        .first()
    )

    if existing_customer:
        return None

    customer = Customer(
        first_name=first_name,
        last_name=last_name,
        email=email,
        password_hash=hash_password(password)
    )

    db.add(customer)
    db.commit()
    db.refresh(customer)

    return customer


def authenticate_customer(
    db,
    email: str,
    password: str
):

    customer = (
        db.query(Customer)
        .filter(Customer.email == email)
        .first()
    )

    if customer is None:
        return None

    if not verify_password(
        password,
        customer.password_hash
    ):
        return None

    return customer


def generate_customer_token(customer: Customer):

    return create_access_token({
        "sub": str(customer.id),
        "email": customer.email
    })