#!/usr/bin/env bash
set -euo pipefail

usage() {
  echo "Brug: $0 {deploy|remove}" >&2
  exit 1
}

[[ $# -eq 1 ]] || usage
action="$1"
[[ "$action" == "deploy" || "$action" == "remove" ]] || usage

script_dir="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd)"

# Hvert element indeholder: Compose-filens sti og Docker Swarm stackens navn.
stacks=(
  "docker/article.yml article"
  "docker/comment.yml comment"
  "docker/draft.yml draft"
  "docker/monitoring.yml monitoring"
  "docker/subscriber.yml subscriber"
  "docker/newsletter.yml newsletter"
  "docker/profanity.yml profanity"
)

for entry in "${stacks[@]}"; do
  read -r relative_file stack_name <<< "$entry"
  file="$script_dir/$relative_file"

  if [[ ! -f "$file" ]]; then
    echo "Filen findes ikke: $file" >&2
    exit 1
  fi

  echo "${action} ${stack_name}..."

  if [[ "$action" == "deploy" ]]; then
    docker stack deploy -c "$file" "$stack_name"
  else
    docker stack rm "$stack_name"
  fi
done
