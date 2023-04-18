FROM alpine:3.17.3

RUN apk add --no-cache --update \
    samba-common-tools \
    samba-client \
    samba-server

COPY ./_docker/samba/smb.conf /etc/samba/smb.conf

CMD ["smbd", "--debug-stdout", "--foreground", "--no-process-group"]